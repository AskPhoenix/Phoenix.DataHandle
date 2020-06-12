#!/bin/bash

echo Enter Facebook Page Access Token:
read token

#echo Enter the list with the domains to whitelist separated by space:
#read -a domains

#json="{ \"whitelisted_domains\": [ "

#for domain in ${domains[@]}
#do
#  json="${json}\"${domain}\", "
#done
#json="${json::-2} ] }"

#curl -X POST -H "Content-Type: application/json" -d "$json" "https://graph.facebook.com/v7.0/me/messenger_profile?access_token=$token"

curl -X POST -H "Content-Type: application/json" -d '{
  "whitelisted_domains": [
    "https://askphoenix.gr/",
    "https://www.askphoenix.gr/",
    "https://pwa.askphoenix.gr/",
    "https://www.pwa.askphoenix.gr/"
  ]
}' "https://graph.facebook.com/v7.0/me/messenger_profile?access_token=$token"

read -p "Press enter to continue..."