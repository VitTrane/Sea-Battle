
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
};

GlobalStateCtrl.prototype.openMenu = function () {
    this.state = 1;
};
GlobalStateCtrl.prototype.createGame = function () {
    this.initWait();
    this.state = 2;
};
GlobalStateCtrl.prototype.gamePreparing = function () {
    this.state = 4;
};

GlobalStateCtrl.prototype.initGame= function(enemyName)
{
    this.gameCtrl = new GameStateCtrl();
    this.gameCtrl.enemyName = enemyName;
    
};
GlobalStateCtrl.prototype.initWait= function () {
    this.waitCtrl = new WaitingStateCtrl();
};

GlobalStateCtrl.prototype.getTopPlayers = function ()
{
    var items = [];
};