var JoinGameStateCtrl = function () {
    this.games = [];
    this.isLoaded = false;
    this.enemyInfo;
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
        url: "/home/JoinGames",
        data: {Creator:this.enemyInfo.Creator,CreationDate:this.enemyInfo.CreationDate},
        success: function (data) {
            if (!data.status) {
                alert(data.message);
            }
        }.bind(this)
    });
};