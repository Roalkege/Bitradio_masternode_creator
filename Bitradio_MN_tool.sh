#!/bin/sh
 
#install requirements
sudo apt-get install build-essential libtool automake autotools-dev autoconf pkg-config libssl-dev libgmp3-dev libevent-dev bsdmainutils -y
sudo add-apt-repository ppa:bitcoin/bitcoin -y
sudo apt-get update
sudo apt-get install libdb4.8-dev libdb4.8++-dev -y
sudo apt-get install libqt5gui5 libqt5core5a libqt5dbus5 qttools5-dev qttools5-dev-tools libprotobuf-dev protobuf-compiler -y
sudo apt-get install libboost-all-dev -y
sudo apt-get install libminiupnpc-dev -y
sudo apt-get install unzip -y
sudo apt-get install cron -y
 
#/.Bitradio erstellen
cd ~
mkdir /root/.Bitradio
 
 
#Bitradiod downloaden
cd ~
wget http://Brocoin.world/download/Bitradiod
 
 
#Blockchain laden
cd ~
echo Downloading the Blokchain This will take serveral minutes.
wget -O blockchain.zip "https://brocoin.world/download/Blockchain.zip"
unzip blockchain.zip
cd Blockchain
mv blk0001.dat /root/.Bitradio/blk0001.dat
mv blk0002.dat /root/.Bitradio/blk0002.dat
mv txleveldb /root/.Bitradio/txleveldb
mv peers.dat /root/.Bitradio/peers.dat
cd ~
rm -rf blockchain.zip
rm -rf Blockchain
  
#Zufallszahelen + IP + Genkey abfragen
cd /root/temp_bitradio/
key=$(ls)
cd ..
rm -r temp_bitradio/

user=$(shuf -i 10000000-100000000000000 -n 1)
password=$(shuf -i 10000000-100000000000000 -n 1)
ip=$(curl https://ipinfo.io/ip)

#.conf erstellen
cat >/root/.Bitradio/Bitradio.conf << EOF
addnode=106.104.90.26
addnode=109.167.137.159
addnode=109.230.195.80
addnode=109.230.195.92
addnode=109.230.255.24
addnode=109.230.255.27
addnode=109.230.255.30
addnode=109.252.91.63
addnode=109.254.101.3
addnode=134.101.139.212
rpcuser=$user
rpcpassword=$password
daemon=1
server=1
listen=1
masternode=1
masternodeprivkey=$key
externalip=$ip
maxconnections=8
EOF
cd ~

#Cronjob erstellen
echo $key
echo Please restart you windows wallet and wait for a full sync of your Masternode!
echo Your node is installed, now go to your Windows wallet and start the Masternode!

#Client starten
chmod 777 Bitradiod
./Bitradiod -daemon
./Bitradiod getblockcount
exit
