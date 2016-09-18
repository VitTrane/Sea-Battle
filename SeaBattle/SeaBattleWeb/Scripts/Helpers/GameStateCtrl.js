var GameStateCtrl = function () {
    this.isStarted = false;
    this.isEnded = false;
    this.isPlayerTurn = false;
    this.enemyName = "Enemy";
    this.playerBoard = new Board();
    this.enemyBoard = new Board();
    this.messages = [];
    
};

/*GameStateCtrl.startListenMessages = function () {
    (function poll() {
        setTimeout(function () {
            $.ajax({
                type: "POST",
                url: "/home/GetMessage",
                success: function (data) {
                    if (data.status) {
                        var mes = this.enemyName + ": " + data.newMessage;
                        var nextItems = this.messages.concat([{ text: mes, id: Date.now() }]);
                        this.messages = nextItems;
                    }
                }.bind(this),
                complete: poll
            });
        }, 2000);
    })();
};*/

GameStateCtrl.prototype.ListenToMessage = function () {
    if (!this.isEnded) {
        $.ajax({
            type: "POST",
            url: "/home/GetMessage",
            success: function (data) {
                if (data.status) {
                    var mes = this.enemyName + ": " + data.newMessage;
                    var nextItems = this.messages.concat([{ text: mes, id: Date.now() }]);
                    this.messages = nextItems;
                }
                else {
                    this.isEnded = data.isEnded;
                }
            }.bind(this)
        });
    }
};

GameStateCtrl.prototype.sendMessage = function (mess) {
    var d = { text: mess };
    var mes = "You: " + mess;
    var nextItems = this.messages.concat([{ text: mes, id: Date.now() }]);
    this.messages = nextItems;
    $.ajax({
        type: "POST",
        url: "/home/SendMessage",
        data: d,
        success: function (data) {
            if (!data.status)
            {
                alert(data.message);
            }
        }.bind(this),
        error: function (e) {
            console.log(e);
            alert('Error! Please try again');
        }
    });

};
GameStateCtrl.prototype.start = function () {
    var d = {map: this.playerBoard.board};
    $.ajax({
        type: "Post",
        url: "/home/StartGame",
        data: d,
        success: function (data) {
            if (data.status) {
                this.isPlayerTurn = data.isPlayerTurn;
                this.enemyName = data.enemy;
                this.isStarted = true;
                if(this.isPlayerTurn)
                {
                    alert('Your turn is first');
                }
                else {
                    alert('Your turn is second');
                }
            }
            else {
                alert(data.message);
            }

        }.bind(this),
        error: function (e) {
            console.log(e);
            alert('Error! Please try again');
        }
    });
};
GameStateCtrl.prototype.exit = function () {
    $.ajax({
        type: "Post",
        url: "/home/ExitGame",
        data: {text: this.isEnded},
        success: function (data) {
            if (! data.status) {
                alert(data.message);
            }

        }.bind(this),
        error: function (e) {
            console.log(e);
            alert('Error! Please try again');
        }
    });
};
GameStateCtrl.prototype.shot = function(i,j)
{
    if (this.isStarted && this.isPlayerTurn && !this.isEnded) {
        var d = { X: j, Y: i };
        $.ajax({
            type: "POST",
            url: "/home/Shot",
            data: d,
            success: function (data) {
                if (data.status) {
                    this.enemyBoard.shot(i, j, data.shotStatus);
                    this.isPlayerTurn = !this.isPlayerTurn;
                }
                else {
                    alert(data.message);
                }

            }.bind(this),
            error: function (e) {
                console.log(e);
                alert('Error! Please try again');
            }
        });
    }
    else
    {
        if(this.isStarted)
        {
            alert('Wait, it is not your turn');
        }
    }
};
GameStateCtrl.prototype.takeShot = function()
{
    if (!this.isPlayerTurn && !this.isEnded) {
        $.ajax({
            type: "Post",
            url: "/home/TakeShot",
            success: function (data) {
                if (data.status) {
                    this.playerBoard.shot(data.x, data.y, data.shotStatus);
                    this.isPlayerTurn = !this.isPlayerTurn;
                   
                }
                else {
                    alert(data.message);
                }

            }.bind(this),
            error: function (e) {
                console.log(e);
                alert('Error! Please try again');
            }
        });
    }
};

