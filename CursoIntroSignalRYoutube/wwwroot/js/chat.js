"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var userId = document.getElementById("userId").value;

connection.on("ReceiveMessage" + userId, function (senderId, receiverId, message) {
    var now = new Date().toLocaleString();

    var msg = now + " - " + senderId + ": " + message;

    var ul = document.getElementById("messagesList");
    var liMessage = document.createElement("li");
    liMessage.textContent = msg;

    ul.insertBefore(liMessage, ul.childNodes[0]);
})

connection.start().catch(function (err) {
    return console.error(err.toString());
})

document.getElementById("sendButton").addEventListener("click", function (event) {
    event.preventDefault();

    var receiverId = document.getElementById("receiverIdInput").value;
    var message = document.getElementById("msgInput").value;

    if (!message) {
        return console.log("Digite uma mensagem");
    }

    if (!receiverId) {
        return console.log("Digite um id de destino");
    }

    connection.invoke("SendMessage", userId, receiverId, message)
        .then(() => {
            var now = new Date().toLocaleString();

            var msg = now + " - Você: " + message;

            var ul = document.getElementById("messagesList");
            var liMessage = document.createElement("li");
            liMessage.textContent = msg;

            ul.insertBefore(liMessage, ul.childNodes[0]);
        })
        .catch(function (err) {
            return console.error(err.toString());
        })
})    