var JoinGameStateCtrl = function () {
    this.games = [];
    this.isLoaded = false;
    this.enemyInfo;
};
JoinGameStateCtrl.prototype.setEnemy = function (i) {
    this.enemyInfo = this.games[i];
};
JoinGameStateCtrl.prototype.getGames = function () {
    $.ajax({
        type: "POST",
        url: "/home/GetGames",
        traditional:true,
        success: function (data) {
            if (data.status) {

                this.games = data.games;
                this.isLoaded = true;
                       
            }
        }.bind(this)
    });
};
JoinGameStateCtrl.prototype.joinGame = function () {
    $.ajax({
        type: "POST",
        url: "/home/JoinGame",
        data: {Creator:this.enemyInfo.Creator,CreationDate:this.enemyInfo.CreationDate},
        success: function (data) {
            if (!data.status) {
                alert(data.message);
            }
        }.bind(this)
    });
};