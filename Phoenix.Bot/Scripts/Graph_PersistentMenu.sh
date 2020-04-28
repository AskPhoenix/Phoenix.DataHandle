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
                    "title": "Αρχικό μενού",
                    "payload": "--persistent-home--"
                },
                {
                    "type": "web_url",
                    "title": "Η ατζέντα μου",
                    "url": "https://nuage.azurewebsites.net/extensions/agenda",
                    "webview_height_ratio": "tall"
                },
		        {
                    "type": "postback",
                    "title": "Περιήγηση",
                    "payload": "--persistent-tutorial--"
                }
            ]
        }
    ]
}' "https://graph.facebook.com/v6.0/me/messenger_profile?access_token=$token"

read -p "Press enter to continue..."