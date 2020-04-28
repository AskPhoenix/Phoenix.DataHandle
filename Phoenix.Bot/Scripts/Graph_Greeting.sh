#!/bin/bash

echo Enter Facebook Page Access Token:
read token

echo Enter the greeting text:
read text

curl -X POST -H "Content-Type: application/json" -d '{
  "greeting": [
    {
      "locale":"default",
      "text":"$text"
    }
  ]
}' "https://graph.facebook.com/v6.0/me/messenger_profile?access_token=$token"

read -p "Press enter to continue..."