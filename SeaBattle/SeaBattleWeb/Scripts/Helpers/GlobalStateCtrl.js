
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
};

GlobalStateCtrl.prototype.openMenu = function () {
    this.state = 1;
};
GlobalStateCtrl.prototype.createGame = function () {
    this.state = 2;
};

GlobalStateCtrl.prototype.initGame= function()
{
    this.gameCtrl = new GameStateCtrl();
};

GlobalStateCtrl.prototype.getTopPlayers = function ()
{
    var items = [];
};