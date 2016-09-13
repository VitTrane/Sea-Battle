var Cell = React.createClass({
    render: function() {
        var classes = "cell " + this.props.color;
		var cellClick = function(e) {
			var temp = e.target;
			if (e.target.props.board.play(e.target.props.row, e.target.props.col))
				e.target.props.onPlay();
				console.log(this);
				};
        return (
            <div className={classes} onClick={cellClick.bind(this)}></div>
        );
    }
});
 
var Row = React.createClass({
 render: function(){
   var i = this.props.row;
   var b = this.props.board;
   var onPlayf = this.props.onPlay;
   var hc = this.props.handleClick;
   return(<div className="FieldRow">
    {this.props.cells.map(function(object, j) {
     return (
      <Cell board={b} color={object} row={i} col={j} onPlay={onPlayf} key={j} />
      );    
    })}
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
    var onPlayf= this.onBoardUpdate;
    var b = this.state.board;
    var hc = this.props.handleClick;
    return(<div className="Field">
      {this.props.board.board.map(function(object,i){        
         return (
         <Row board={b} cells={object} row={i} onPlay={onPlayf} key={i}/>
         );
       })}
      </div>);
          }
 
});