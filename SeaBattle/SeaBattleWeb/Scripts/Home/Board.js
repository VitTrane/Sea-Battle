var Board = function() {

    this.size = 10;
    this.board = this.create_board();
    this.ships = []   
};
var CONST ={};
CONST.AvailableShips = [4,3,2,1]
CONST.Clean = 'clean';
CONST.Miss = 'miss';
CONST.Damaged = 'damaged';
CONST.Kiiled = 'killed';
CONST.Ship = 'ship';

CONST.Player = 1;
CONST.Enemy = 2;


Board.prototype.create_board = function() {
    var m = [];
    for (var i = 0; i <10; i++) {
        m[i] = [];
        for (var j = 0; j < 10; j++)
            m[i][j] = CONST.Clean;
    }
    return m;
};

Board.prototype.play = function (i, j) {
    if (this.board[i][j] != CONST.Clean)
        return false;
    else
    {
        this.board[i][j] = CONST.Ship;
        return true;
    }
};