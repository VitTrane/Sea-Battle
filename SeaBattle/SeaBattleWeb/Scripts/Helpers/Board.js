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
Board.prototype.isReadyForGame = function()
{
    for (var i =0; i < 4; i++)
    {
        if(this.AvailableShips[i] != 0)
            return false;
    }
    return true;
};

Board.prototype.btnCommandReact = function(i, j, bt, isHorizontal)
{
    var decks = parseInt(bt, 10);
    switch (decks)
    {
        case 0:
            {
                if(this.board[i][j]==CONST.Ship)
                {
                    let isHoriz = this.udCellsClean(i, j);
                    let temp = this.getUpperLeftShipCell(i, j, isHoriz);
                    return (this.deleteShip(temp.i, temp.j,isHoriz));
                }
                else
                {
                    return false;
                }
                
            }
        case 5:
            {
                if(this.board[i][j]==CONST.Ship)
                {
                    let isHoriz = this.udCellsClean(i, j);
                    let temp = this.getUpperLeftShipCell(i, j, isHoriz);
                    return (this.rotateShip(temp.i, temp.j,isHoriz));
                }
                else
                {
                    return false;
                }
            }
        default:
            return (this.createShip(i,j,decks,isHorizontal));
    }
};

Board.prototype.shot = function(i,j,status)
{
    switch(status)
    {
        case "killed":
            var isHoriz = this.udCellsClean(i,j);
            var fc = this.getUpperLeftShipCell(i,j,isHoriz);
            var count = this.getDecksCount(i,j,isHoriz);
            if(isHoriz)
            {
                for(var c =0; c < count;c++)
                {
                    this.board[fc.i][fc.j+c]=CONST.Kiiled;
                }
            }
            else
            {
                for(var c =0; c < count;c++)
                {
                    this.board[fc.i+c][fc.j]=CONST.Kiiled;
                }

            }
            break;
        case "damaged":
            this.board[i][j]=CONST.Damaged;
            break;
        default:
            this.board[i][j]=CONST.Miss;
            break;                   
    }
}

Board.prototype.rotateShip = function(i, j, isHorizontal)
{
    var decks = this.getDecksCount(i,j,isHorizontal);
    this.deleteShip(i,j,isHorizontal);
    var t = !isHorizontal;
    if(this.createShip(i,j,decks,t))
    {
        return true;
    }
    else
    {
        this.createShip(i,j,decks,isHorizontal);
        return false;
    }
};
Board.prototype.getDecksCount = function(i,j,isHorizontal)
{
    var count = 0;
    if (isHorizontal)
    {
        while(j+count<this.size && (this.board[i][j+count]==CONST.Ship || this.board[i][j+count]==CONST.Damaged || this.board[i][j+count]==CONST.Kiiled))
        {
            count++;
        }
    }
    else 
    {
        while(i+count<this.size && (this.board[i+count][j]==CONST.Ship || this.board[i][j+count]==CONST.Damaged || this.board[i][j+count]==CONST.Kiiled))
        {
            count++;
        }
    } 
    return count;
};
Board.prototype.deleteShip = function(i, j, isHorizontal)
{
    var col = 0;
    if (isHorizontal)
    {
        while(j+col<this.size && this.board[i][j+col]==CONST.Ship)
        {
            this.board[i][j+col]=CONST.Clean;
            col++;
        }
        this.AvailableShips[col-1]++;
        return true;
    }
    else 
    {
        while(i+col<this.size && this.board[i+col][j]==CONST.Ship)
        {
            this.board[i+col][j]=CONST.Clean;
            col++;
        }
        this.AvailableShips[col-1]++;
        return true;
    }

};

Board.prototype.getUpperLeftShipCell = function (i, j, isHorizontal)
{
    if (isHorizontal)
    {
        var col = j;
        while(col-1 >= 0 && (this.board[i][col-1]==CONST.Ship || this.board[i][col-1]==CONST.Damaged || this.board[i][col-1]==CONST.Kiiled))
        {
            col--;
        }
        return { i:i, j:col};
    }
    else
    {
        var row = i;
        while (row-1 >= 0 && (this.board[row-1][j] == CONST.Ship || this.board[row-1][j] == CONST.Damaged || this.board[row-1][j] == CONST.Kiiled)) {
            row--;
        }
        return { i: row,j: j };

    }

};

Board.prototype.createShip = function (i, j, decks, isHorizontal) {
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
            return (this.board[i + 1][j] == CONST.Clean || this.board[i + 1][j] == CONST.Miss);
        case this.size-1:
            return (this.board[i - 1][j] == CONST.Clean || this.board[i - 1][j] == CONST.Miss);
        default:
            return (this.board[i + 1][j] == CONST.Clean ||this.board[i + 1][j] == CONST.Miss) && (this.board[i - 1][j] == CONST.Clean || this.board[i - 1][j] == CONST.Miss);
    }
}
Board.prototype.lrCellsClean = function (i, j) {
    switch (j) {
        case 0:
            return (this.board[i][j + 1] == CONST.Clean);
        case this.size-1:
            return (this.board[i][j - 1] == CONST.Clean);
        default:
            return (this.board[i][j + 1] == CONST.Clean && this.board[i][j - 1] == CONST.Clean);
    }
}

