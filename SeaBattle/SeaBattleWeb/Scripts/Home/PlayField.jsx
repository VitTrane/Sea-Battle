var Cell = React.createClass({
    render: function() {
        var classes = "cell " + this.props.color;
		var cellClick = function(e) {
			var temp = this;
			if (this.props.board.play(this.props.row, this.props.col))
				this.props.onPlay();
				};
        return (
            <div className={classes} onClick={cellClick.bind(this)}></div>
        );
    }
});
 
var Row = React.createClass({
 render: function(){
   var getCells = function(object,j){
					 return (<Cell board={this.props.board} color={object} row={this.props.row} col={j} onPlay={this.props.onPlay} key={j} />);
				}
   return(<div className="FieldRow">
    {
		this.props.cells.map(getCells.bind(this))
	}
   </div>);  
 }
     
});
 
var PlayField = React.createClass({
 
 getInitialState: function() {
        return {board: this.props.board, isReady:this.props.isReady, isPlayer: this.props.isPlayer};
    },
 onBoardUpdate: function() {
        this.setState({board: this.props.board});
    },
 
 render: function() {
	var getRows = function(object,i){
					 return (<Row board={this.state.board} cells={object} row={i} onPlay={this.onBoardUpdate} key={i}/>);
				}		
    return(
		<div className="Field">
		{
			this.props.board.board.map(getRows.bind(this))
		}
      </div>);

	}
          
});