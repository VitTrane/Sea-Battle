
var States ={};
States.Registration = 0;
States.Menu = 1;
States.Waiting = 2;
States.JoinGame = 3;
States.Game = 4;
States.Statistics = 5;

var GlobalStateCtrl = function () {
    this.state = 0;
    this.gameCtrl;
    this.waitCtrl;
    this.joinCtrl;
    this.statisticCtrl;
};

GlobalStateCtrl.prototype.openMenu = function () {
    this.state = 1;
};
GlobalStateCtrl.prototype.createGame = function () {
    this.waitCtrl = new WaitingStateCtrl();
    this.state = 2;
};
GlobalStateCtrl.prototype.gamePreparing = function (enemyName) {
    this.initGame(enemyName);
    this.state = 4;
};
GlobalStateCtrl.prototype.joinGame = function () {
    this.joinCtrl = new JoinGameStateCtrl();
    this.state = 3;
};

GlobalStateCtrl.prototype.initGame= function(enemyName)
{
    this.gameCtrl = new GameStateCtrl();
    this.gameCtrl.enemyName = enemyName;   
};
GlobalStateCtrl.prototype.seeStatistic = function () {
    this.statisticCtrl = new StatisticsStateCtrl();
    this.state = 5;
};
GlobalStateCtrl.prototype.logOut = function () {
    $.ajax({
        type: "Post",
        url: "/home/LogOut",
        success: function (data) {
            if (!data.status) {
                alert(data.message);
            }
        }.bind(this),
        error: function (e) {
            console.log(e);
            alert('Error! Please try again');
        }
    });

    this.state = 0;
};

