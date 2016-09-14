var Cell = React.createClass({
    render: function() {
        var classes = "cell " + this.props.color;
		var cellClick = function(e) {
			var temp = this;
			if (this.props.board.createBoard(this.props.row, this.props.col,3,false))
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
 
var Field = React.createClass({
 
 getInitialState: function() {
        return {board: this.props.board};
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

var PlayField = React.createClass({
	getInitialState: function() {
        return {board: this.props.board, decks:4};
    },
	onDeckChanged: function (e) {
		this.setState({
		  decks: e.currentTarget.value
		  });
	},
	render: function() {
		
		return(
			<div>
				<div className="col">
					<Field board={board} isReady={false}/>
				</div>
					<form action=""> 
						<p>
							<input className="with-gap" name="group1" type="radio" id="d4" value={4} checked={this.state.decks === 4} onChange={this.onDeckChanged}/>
								  <label for="d4">4</label>
						</p>
						<p>
							<input className="with-gap" name="group1" type="radio" id="d3" value={3} checked={this.state.decks === 3} onChange={this.onDeckChanged}/>
								  <label for="d3">3</label>
						</p>
						<p>
							<input className="with-gap" name="group1" type="radio" id="d2" value={2} checked={this.state.decks === 2} onChange={this.onDeckChanged}/>
								  <label for="d4">2</label>
						</p>
						<p>
							<input className="with-gap" name="group1" type="radio" id="d1" value={1} checked={this.state.decks === 4} onChange={this.onDeckChanged}/>
								  <label for="d4">1</label>
						</p>
					</form>
			</div>
		);
	}

});