#!/bin/bash

echo Enter Facebook Page Access Token:
read token

curl -X POST -H "Content-Type: application/json" -d '{
  "get_started": {"payload": "--persistent-get-started--"}
}' "https://graph.facebook.com/v6.0/me/messenger_profile?access_token=$token"

read -p "Press enter to continue..."