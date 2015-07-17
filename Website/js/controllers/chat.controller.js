(function () {
    'use strict';

    angular.module('ticketsApp')
        .controller("ChatController", ChatController);

    ChatController.$inject = ['$scope'];
    function ChatController($scope) {
        var vm = this;
        vm.mySocket;
        vm.chatStarted = false;
        vm.username = "";
        vm.teamName = "";
        vm.onlineUsers = [];
        vm.groupMessages = [];
        vm.teamMessages = [];
        vm.privateChats = {};
        vm.outgoingGroupMessage = '';
        vm.outgoingTeamMessage = '';
        vm.activateChat = activateChat;
        vm.sendGroupMessage = sendGroupMessage;
        vm.sendTeamMessage = sendTeamMessage;
        vm.createPrivateChat = createPrivateChat;
        vm.sendPrivateMessage = sendPrivateMessage;

        function activateChat() {
            vm.chatStarted = true;
            vm.mySocket = io.connect('http://localhost:3000');
            vm.mySocket.emit('add', vm.username);
            vm.mySocket.emit('join', vm.teamName);

            vm.mySocket.on('online users', function (data) {
                vm.onlineUsers = data;
                $scope.$apply();
            });

            vm.mySocket.on('group chat', function (data) {
                if (data.room === "everyone") {
                    vm.groupMessages.push({ author: data.author, message: data.message });
                } else if (data.room === vm.teamName) {
                    vm.teamMessages.push({ author: data.author, message: data.message });
                }
                $scope.$apply();
            });

            vm.mySocket.on('private message', function (data) {
                createPrivateChat(data.id, data.author);
                vm.privateChats[data.id].messages.push({ author: data.author, message: data.message });
                $scope.$apply();
            });
        }

        function sendGroupMessage() {
            vm.mySocket.emit('group chat', { room: "everyone", author: vm.username, message: vm.outgoingGroupMessage });
            vm.outgoingGroupMessage = "";
        }

        function sendTeamMessage() {
            vm.mySocket.emit('group chat', { room: vm.teamName, author: vm.username, message: vm.outgoingTeamMessage });
            vm.outgoingTeamMessage = "";
        }

        function sendPrivateMessage(targetId) {
            vm.mySocket.emit('private message', { id: targetId, author: vm.username, message: vm.privateChats[targetId].outgoingMessage });
            vm.privateChats[targetId].outgoingMessage = '';
        }

        function createPrivateChat(id, username) {
            if (!(id in vm.privateChats)) {
                var privateChat = { name: username, messages: [], outgoingMessage: "", id: id };
                vm.privateChats[id] = privateChat;
            }
        }
    }
})();