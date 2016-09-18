var WaitingStateCtrl = function () {
    this.isFounded = false;
    this.enemyName = "";
};
WaitingStateCtrl.prototype.waiting = function () {
    $.ajax({
        type: "POST",
        url: "/home/CreateGame",
        success: function (data) {
            if (data.status) {
                this.isFounded = true;
                this.enemyName = data.userName;
            }
        }.bind(this),
    });

};
WaitingStateCtrl.prototype.exit = function () {
    $.ajax({
        type: "POST",
        url: "/home/StopWaitForPlayer",
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

};