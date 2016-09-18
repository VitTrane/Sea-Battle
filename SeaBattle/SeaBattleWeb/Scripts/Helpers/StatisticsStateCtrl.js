var StatisticsStateCtrl = function () {
    this.games = [];
    this.players = [];
};

StatisticsStateCtrl.prototype.getGames = function () {
    $.ajax({
        type: "POST",
        url: "/home/GetLastGames",
        traditional: true,
        success: function (data) {
            if (data.status) {

                this.games = data.games;
            }
        }.bind(this)
    });
};
StatisticsStateCtrl.prototype.getPlayers = function () {
    $.ajax({
        type: "POST",
        url: "/home/GetTopPlayers",
        traditional: true,
        success: function (data) {
            if (data.status) {

                this.players = data.players;
            }
        }.bind(this)
    });
};