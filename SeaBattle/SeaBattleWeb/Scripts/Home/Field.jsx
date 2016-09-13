var Cell = React.createClass({
    handleClick: function() {
        if (this.props.board.play(this.props.row, this.props.col))
            this.props.onPlay();
    },
    render: function() {
        var classes = "cell " + this.props.color;

        return (
            <div onClick={this.handleClick} 
                className={classes}></div>
        );
    }
});

var PlayField = React.createClass({
	getInitialState: function() {
        return {'board': this.props.board};
    },
    onBoardUpdate: function() {
        this.setState({"board": this.props.board});
    },
    render: function() {
        var cells = [];
        for (var i = 0; i < 10; i++)
            for (var j = 0; j < 10; j++)
                cells.push(Cell({
                    board: this.state.board,
                    color: this.state.board.board[i][j],
                    row: i,
                    col: j,
                    onPlay: this.onBoardUpdate.bind(this)
                }));				
        return <div className="Field">{cells}</div>;
    }
});