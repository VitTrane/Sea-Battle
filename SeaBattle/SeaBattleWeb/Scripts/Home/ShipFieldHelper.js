var ShipFieldHelper = {

    fieldSize: 0,

    sortedShipCells: function (cells)
    {
        if (cells.length < 2) return cells;

        cells.sort(function (p1, p2) {
            if (p1.x < p2.x)
                return -1;
            if (p1.x > p2.x)
                return 1;
            if (p1.x == p2.x) {
                if (p1.y < p2.y)
                    return -1;
                if (p1.y > p2.y)
                    return 1;
                if (p1.y == p2.y)
                    return 0;
            }
        });
    },

    getUpperLeftShipCell: function (cells)
    {
        this.sortedShipCells(cells);
        return cells[0];
    },

    getBottomRightShipCell: function (cells)
    {
        this.sortedShipCells(cells);
        return cells[cells.length - 1];
    },

    getCellsAround: function (shipCells, withoutCornerCells)
    {
        var UpperLeft = this.getUpperLeftShipCell(shipCells);
        var BottomRight = this.getBottomRightShipCell(shipCells);

        var minX = Math.max(0, topLeft.x - 1);
        var minY = Math.max(0, topLeft.y - 1);
        var maxX = Math.min(this.fieldSize, BottomRight.x + 1);
        var maxY = Math.min(this.fieldSize, BottomRight.y + 1);

        var cornerCells = [];
        if (withoutCornerCells) {
            if (minX < UpperLeft.x && minY < UpperLeft.y)
                cornerCells.push({ x: minX, y: minY });
            if (minX < UpperLeft.x && maxY > BottomRight.y)
                cornerCells.push({ x: minX, y: maxY });
            if (maxX > BottomRight.x && minY < UpperLeft.y)
                cornerCells.push({ x: maxX, y: minY });
            if (maxX > BottomRight.x && maxY > BottomRight.y)
                cornerCells.push({ x: maxX, y: maxY });
        }

        var aroundCells = [];
        for (var x = minX; x <= maxX; x++) {
            for (var y = minY; y <= maxY; y++) {
                if (!(x >= UpperLeft.x && x <= BottomRight.x && y >= UpperLeft.y && y <= BottomRight.y))
                {
                    var cell = { x: x, y: y };
                    if (withoutCornerCells) {
                        if (!_.any(cornerCells, cell)) {
                            aroundCells.push(cell);
                        }
                    } else {
                        aroundCells.push({ x: x, y: y });
                    }
                }
            }
        }

        return aroundCells;
    },

    canBeDropped: function (dropCells, droppedShipId, ships)
    {
        if (!this.areCellsValid(dropCells)) {
            return false;
        }
        var dropArea = dropCells.concat(this.getCellsAround(dropCells));

        for (var i = 0; i < dropArea.length; i++) {
            var cell = dropArea[i];
            if (_.find(ships, function (ship) {
                if (ship.id == droppedShipId) {
                  return false;
            }
                var takenCell = _.find(ship.cells, function (shipCell) {
                  return (shipCell.x == cell.x && shipCell.y == cell.y);
            });
                return takenCell ? true : false;
            })) {
                return false;
            }
        }
        return true;
    },

    isCellValid: function (cell) {
        return (cell.x < this.fieldSize && cell.y < this.fieldSize);
    },

    areCellsValid: function (cells) {
        for (var i = 0; i < cells.length; i++) {
            if (!this.isCellValid(cells[i])) {
                return false;
            }
        }
        return true;
    },

    getDropCellsForConfigItem: function (cell, ship) {
        var result = [];
        for (var i = 0; i < ship.size; i++) {
            result.push({ x: cell.x + i, y: cell.y });
        }
        return result;
    },

    getDroppedCellsForShip: function (cell, originalCells) {
        var topLeft = this.getUpperLeftShipCell(originalCells);
        var deltaX = cell.x - topLeft.x;
        var deltaY = cell.y - topLeft.y;

        return originalCells.map(function (cell) {
            return { x: cell.x + deltaX, y: cell.y + deltaY }
        });
    }
}