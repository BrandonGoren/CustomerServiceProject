var app = require('express')();
var http = require('http').Server(app);
var io = require('socket.io')(http);
var rooms = ['everyone'];
var clients = [];
var clientsHash = {};
var idCount = 0;

app.get('/', function (req, res) {
    res.send('<h1>Hello world</h1>');
});

io.on('connection', function (socket) {

    socket.on('group chat', function (data) {
        if (data.room !== "everyone") {
            io.sockets.in(data.room).emit('group chat', data);
        } else {
            io.sockets.emit('group chat', data);
        }
    });

    socket.on('add', function (user) {
        socket.userId = ++idCount;
        socket.username = user;
        clientsHash[socket.userId] = { username: user, socketId: socket.id };
        console.log(socket.username + " has entered the chat");
        io.sockets.emit('online users', hashToList(clientsHash));
    });

    socket.on('private message', function (data) {
        if (data.id in clientsHash) {
            var targetUser = clientsHash[data.id];
            io.sockets.connected[targetUser.socketId].emit('private message', { id: socket.userId, author: socket.username, message: data.message })
            io.sockets.connected[socket.id].emit('private message', { id: targetUser.id, author: socket.username, message: data.message })
        } else {
            io.sockets.connected[socket.id].emit('private message', { id: data.id, author: 'system', message: 'user is offline' })
        }
    });

    socket.on('join', function (room) {
        if (rooms.indexOf(room) == -1) {
            rooms.push(room);
        }
        console.log(socket.username + " has joined " + room);
        socket.join(room);
    });

    socket.on('disconnect', function () {
        delete clientsHash[socket.userId];
        io.sockets.emit('online users', hashToList(clientsHash));
        console.log(socket.username + " has left the building");
    });
});

http.listen(3000, function () {
    console.log('listening on *:3000');
});

function hashToList(hash) {
    var output = [];
    for (var x in hash) {
        var dataItem = hash[x];
        dataItem.id = x;
        output.push(dataItem);
    }
    return output;
}