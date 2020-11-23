#!/bin/bash
NAME="Init"

if [ "$1" ]; then
	NAME=$1
fi

echo ">> $NAME <<"

if [ ! "$NAME" ];then
   exit 1
fi


echo -e  "\e[96mDrop-Database\e[93m"

dotnet ef database drop --context 'mini_spotify.DAL.AppDbContext'

echo -e "\e[96mRemove-Migration\e[93m"

dotnet ef migrations remove --context 'mini_spotify.DAL.AppDbContext'

echo -e "\e[96mAdd-Migration \e[95m$NAME\e[93m" 

dotnet ef migrations add $NAME --context 'mini_spotify.DAL.AppDbContext' -o ./DAL/Migrations || exit 1

echo -e "\e[96mUpdate-Database\e[93m"

dotnet ef database update $NAME --context 'mini_spotify.DAL.AppDbContext' || exit 1

exit 0