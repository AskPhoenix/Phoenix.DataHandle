#!/bin/bash

echo Enter Facebook Page Access Token:
read token

curl -X POST -H "Content-Type: application/json" -d '{
    "persistent_menu": [
        {
            "locale": "default",
            "composer_input_disabled": false,
            "call_to_actions": [
                {
                    "type": "postback",
                    "title": "🏠 Αρχικό μενού",
                    "payload": "--persistent-home--"
                },
		        {
                    "type": "postback",
                    "title": "ℹ️ Τι μπορώ να κάνω!",
                    "payload": "--persistent-tutorial--"
                },
                {
                    "type": "postback",
                    "title": "👍 Αφήστε ένα σχόλιο!",
                    "payload": "--persistent-feedback--"
                }
            ]
        }
    ]
}' "https://graph.facebook.com/v7.0/me/messenger_profile?access_token=$token"

read -p "Press enter to continue..."