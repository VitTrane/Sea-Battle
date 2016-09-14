var Board = function() {

    this.size = 10;
    this.board = this.create_board();
    this.isReady = false;
    this.AvailableShips = [4,3,2,1]
};
var CONST ={};
CONST.Clean = 'clean';
CONST.Miss = 'miss';
CONST.Damaged = 'damaged';
CONST.Kiiled = 'killed';
CONST.Ship = 'ship';

CONST.Player = 1;
CONST.Enemy = 2;


Board.prototype.create_board = function() {
    var m = [];
    for (var i = 0; i <this.size; i++) {
        m[i] = [];
        for (var j = 0; j < this.size; j++)
            m[i][j] = CONST.Clean;
    }
    return m;
};

Board.prototype.play = function (i, j) {
    var temp = this.board[i][j];
    if (this.board[i][j] != CONST.Clean)
        return false;
    else
    {
        this.board[i][j] = CONST.Ship;
        return true;
    }
};
Board.prototype.createBoard = function (i, j, bt, isHorizontal) {
    decks = parseInt(bt, 10);
    if (this.AvailableShips[decks - 1] != 0)
    {
        if (this.canBeCreated(i, j, decks, isHorizontal))
        {
            if (isHorizontal)
            {
                for (var col = 0; col < decks; col++) {
                    this.board[i][j+col] = CONST.Ship;
                }
            }
            else
            {
                for (var row = 0; row < decks; row++) {
                    this.board[i+row][j] = CONST.Ship;
                }
            }
            this.AvailableShips[decks - 1]--;
            return true;
        }
        else
        {
            return false;
        }
    }
    else
    {
        return false;
    }
};
Board.prototype.canBeCreated = function(i,j, decks, isHorizontal)
{
    var iMin = (i == 0) ? i : i - 1;
    var jMin = (j == 0) ? j : j - 1;
    var iMax = 0;
    var jMax = 0;
    if(isHorizontal)
    {
        if (j + decks - 1 > this.size)
        {
            return false;
        }
            jMax = ((j + decks) == this.size) ? j + decks - 1 : j + decks;
            iMax = (i == this.size - 1) ? i : i + 1; 
    }
    else
    {
        if (i + decks - 1 > 10)
        {
            return false;
        }
            iMax = ((i + decks) == this.size) ? i + decks - 1 : i + decks;
             jMax = (j == this.size - 1) ? j : j + 1;
    }
    for (var r = iMin; r < iMax + 1; r++) {
        for (var c = jMin; c < jMax + 1; c++) {
            if (this.board[r][c] != CONST.Clean) {
                return false;
            }

        }
    }
    return true;
}

Board.prototype.udCellsClean = function (i,j)
{
    switch (i)
    {
        case 0:
            return (this.board[i + 1][j] == CONST.Clean);
        case this.size:
            return (this.board[i - 1][j] == CONST.Clean);
        default:
            return (this.board[i + 1][j] == CONST.Clean && this.board[i - 1][j] == CONST.Clean);
    }
}
Board.prototype.lrCellsClean = function (i, j) {
    switch (j) {
        case 0:
            return (this.board[i][j + 1] == CONST.Clean);
        case this.size:
            return (this.board[i][j - 1] == CONST.Clean);
        default:
            return (this.board[i][j + 1] == CONST.Clean && this.board[i][j - 1] == CONST.Clean);
    }
}

