var Cell = React.createClass({
    render: function() {
        var classes = "cell " + this.props.color;
		var cellClick = function(e) {
			if (this.props.board.btnCommandReact(this.props.row, this.props.col,this.props.btAct,false))
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
					 return (<Cell board={this.props.board} color={object} row={this.props.row} col={j} onPlay={this.props.onPlay} btAct = {this.props.btAct} key={j} />);
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
 render: function() {
	var getRows = function(object,i){
					 return (<Row board={this.state.board} cells={object} row={i} onPlay={this.props.onBoardUpdate} btAct = {this.props.btAct} key={i}/>);
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
        return {board: this.props.board, bt:5};
    },
	render: function() {
		var clickSt = function(e)
		{
			this.setState({bt: e.currentTarget.id});
		};
		if(this.state.isReady)
		return(
			<div>
				<div className="row">
					<Field board={board} isReady={false} btAct={this.state.bt}/>
				</div>
				<div className="row">
					{(() => {
							switch (this.state.bt) {
							  case "5":  return (<form action=""> 
														
														<a className="waves-effect waves-teal btn" id="4" onClick={clickSt.bind(this)}>4</a> 
														<a className="waves-effect waves-teal btn" id="3" onClick={clickSt.bind(this)}>3</a> 
														<a className="waves-effect waves-teal btn" id="2" onClick={clickSt.bind(this)}>2</a> 
														<a className="waves-effect waves-teal btn" id="1" onClick={clickSt.bind(this)}>1</a>
														<p><a className="waves-effect waves-teal btn" id="0"onClick={clickSt.bind(this)}>Delete</a>
														<a className="waves-effect red lighten-2 btn" id="5" onClick={clickSt.bind(this)}>Rotate</a></p> 
													</form>);
							  case "4":	return (
													<form action=""> 
														<a className="waves-effect red lighten-2 btn" id="4" onClick={clickSt.bind(this)}>4</a> 
														<a className="waves-effect waves-teal btn" id="3" onClick={clickSt.bind(this)}>3</a> 
														<a className="waves-effect waves-teal btn" id="2" onClick={clickSt.bind(this)}>2</a> 
														<a className="waves-effect waves-teal btn" id="1" onClick={clickSt.bind(this)}>1</a> 
														<p><a className="waves-effect waves-teal btn" id="0"onClick={clickSt.bind(this)}>Delete</a>
														<a className="waves-effect waves-teal btn" id="5" onClick={clickSt.bind(this)}>Rotate</a></p>
													</form>
												);
							  case "3":  return (
													<form action=""> 
														
														<a className="waves-effect waves-teal btn" id="4" onClick={clickSt.bind(this)}>4</a> 
														<a className="waves-effect red lighten-2 btn" id="3" onClick={clickSt.bind(this)}>3</a> 
														<a className="waves-effect waves-teal btn" id="2" onClick={clickSt.bind(this)}>2</a> 
														<a className="waves-effect waves-teal btn" id="1" onClick={clickSt.bind(this)}>1</a> 
														<p><a className="waves-effect waves-teal btn" id="0"onClick={clickSt.bind(this)}>Delete</a>
														<a className="waves-effect waves-teal btn" id="5" onClick={clickSt.bind(this)}>Rotate</a></p> 
													</form>
													);
							  case "2":  return (
													<form action=""> 
														
														<a className="waves-effect waves-teal btn" id="4" onClick={clickSt.bind(this)}>4</a> 
														<a className="waves-effect waves-teal btn" id="3" onClick={clickSt.bind(this)}>3</a> 
														<a className="waves-effect red lighten-2 btn" id="2" onClick={clickSt.bind(this)}>2</a> 
														<a className="waves-effect waves-teal btn" id="1" onClick={clickSt.bind(this)}>1</a> 
														<p><a className="waves-effect waves-teal btn" id="0"onClick={clickSt.bind(this)}>Delete</a>
														<a className="waves-effect waves-teal btn" id="5" onClick={clickSt.bind(this)}>Rotate</a></p>  
													</form>
													);
							  case "1":  return (
													<form action=""> 
														
														<a className="waves-effect waves-teal btn" id="4" onClick={clickSt.bind(this)}>4</a> 
														<a className="waves-effect waves-teal btn" id="3" onClick={clickSt.bind(this)}>3</a> 
														<a className="waves-effect waves-teal btn" id="2" onClick={clickSt.bind(this)}>2</a> 
														<a className="waves-effect red lighten-2 btn" id="1" onClick={clickSt.bind(this)}>1</a> 
														<p><a className="waves-effect waves-teal btn" id="0"onClick={clickSt.bind(this)}>Delete</a> 
														<a className="waves-effect waves-teal btn" id="5" onClick={clickSt.bind(this)}>Rotate</a></p>
													</form>
													);
							  default:   return (
													<form action=""> 
														 
														<a className="waves-effect waves-teal btn" id="4" onClick={clickSt.bind(this)}>4</a> 
														<a className="waves-effect waves-teal btn" id="3" onClick={clickSt.bind(this)}>3</a> 
														<a className="waves-effect waves-teal btn" id="2" onClick={clickSt.bind(this)}>2</a> 
														<a className="waves-effect waves-teal btn" id="1" onClick={clickSt.bind(this)}>1</a> 
														<p><a className="waves-effect red lighten-2 btn" id="0"onClick={clickSt.bind(this)}>Delete</a>
														<a className="waves-effect waves-teal btn" id="5" onClick={clickSt.bind(this)}>Rotate</a></p>
													</form>	
												);
							}
					})()}
				</div>
			</div>
		);
	}

});