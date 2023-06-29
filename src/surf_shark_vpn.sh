#!/bin/bash

token=null
api="https://api.surfshark.com"
user_agent="SurfsharkAndroid/2.8.7.4 com.surfshark.vpnclient.android/release/playStore/208070400 device/mobile"

function get_server_clusters() {
	curl --request GET \
		--url "$api/v4/server/clusters/all" \
		--user-agent "$user_agent" \
		--header "content-type: application/json"
}

function get_own_info() {
	curl --request GET \
		--url "$api/v1/server/user" \
		--user-agent "$user_agent" \
		--header "content-type: application/json"
}

function register() {
	# 1 - email: (string): <email>
	# 2 - password: (string): <password>
	curl --request POST \
		--url "$api/v1/account/users" \
		--user-agent "$user_agent" \
		--header "content-type: application/json" \
		--data '{
			"email": "'$1'",
			"password": "'$2'"
		}'
}

function login() {
	# 1 - email: (string): <email>
	# 2 - password: (string): <password>
	response=$(curl --request POST \
		--url "$api/v1/auth/login" \
		--user-agent "$user_agent" \
		--header "content-type: application/json" \
		--data '{
			"username": "'$1'",
			"password": "'$2'"
		}')
	if [ -n $(jq -r ".token" <<< "$response") ]; then
		token=$(jq -r ".token" <<< "$response")
	fi
	echo $response
}

function get_account_info() {
	curl --request GET \
		--url "$api/v1/account/users/me" \
		--user-agent "$user_agent" \
		--header "content-type: application/json" \
		--header "authorization: Bearer $token"
}

function get_account_notifications() {
	curl --request GET \
		--url "$api/v2/notification/me" \
		--user-agent "$user_agent" \
		--header "content-type: application/json" \
		--header "authorization: Bearer $token"
}

function get_notification_auth() {
	curl --request GET \
		--url "$api/v1/notification/authorization" \
		--user-agent "$user_agent" \
		--header "content-type: application/json" \
		--header "authorization: Bearer $token"
}
