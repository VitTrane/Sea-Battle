var PreparingCell = React.createClass({
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
var PlayerCell = React.createClass({
    render: function() {
        var classes = "cell " + this.props.color;
        return (
            <div className={classes}></div>
        );
    }
});
var EnemyCell = React.createClass({
    render: function() {
        var classes = "cell " + this.props.color;
		var cellClick = function(e) {
				this.props.onShot(this.props.row,this.props.col);
				this.props.onPlay();
				};
        return (
            <div className={classes} onClick={cellClick.bind(this)}></div>
        );
    }
});
 
var PreparingRow = React.createClass({
 render: function(){
   var getCells = function(object,j){
					 return (<PreparingCell board={this.props.board} color={object} row={this.props.row} col={j} onPlay={this.props.onPlay} btAct = {this.props.btAct} key={j} />);
				}
   return(<div className="FieldRow">
    {
		this.props.cells.map(getCells.bind(this))
	}
   </div>);  
 }
     
});
var PlayerRow = React.createClass({
 render: function(){
   var getCells = function(object,j){
					 return (<PlayerCell board={this.props.board} color={object} row={this.props.row} col={j} key={j} />);
				}
   return(<div className="FieldRow">
    {
		this.props.cells.map(getCells.bind(this))
	}
   </div>);  
 }
     
});
var EnemyRow = React.createClass({
 render: function(){
   var getCells = function(object,j){
					 return (<EnemyCell board={this.props.board} color={object} row={this.props.row} col={j} onPlay={this.props.onPlay} onShot = {this.props.onShot} key={j} />);
				}
   return(<div className="FieldRow">
    {
		this.props.cells.map(getCells.bind(this))
	}
   </div>);  
 }
     
});
 
var PreparingField = React.createClass({
 render: function() {
	var getRows = function(object,i){
					 return (<PreparingRow board={this.props.board} cells={object} row={i} onPlay={this.props.onBoardUpdate} btAct = {this.props.btAct} key={i}/>);
				}		
    return(
		<div className="Field">
		{
			this.props.board.board.map(getRows.bind(this))
		}
      </div>);

	}          
});
var EnemyField = React.createClass({
 render: function() {
	var getRows = function(object,i){
					 return (<EnemyRow board={this.props.board} cells={object} row={i} onPlay={this.props.onUpdate} onShot = {this.props.onShot} key={i}/>);
				}		
    return(
		<div className="Field">
		{
			this.props.board.board.map(getRows.bind(this))
		}
      </div>);

	}          
});
var PlayerField = React.createClass({
 render: function() {
	var getRows = function(object,i){
					 return (<PlayerRow board={this.props.board} cells={object} row={i}  key={i}/>);
				}		
    return(
		<div className="Field">
		{
			this.props.board.board.map(getRows.bind(this))
		}
      </div>);

	}          
});


var PlayPreparingField = React.createClass({
	getInitialState: function() {
        return {bt:5};
    },
	render: function() {
		var clickSt = function(e)
		{
			this.setState({bt: e.currentTarget.id});
		};
		
		return(
			<div>
				<div className="row">
					<PreparingField  board={this.props.board} onBoardUpdate = {this.props.onUpdate} btAct={this.state.bt}/>
				</div>
				<div className="row">
					{(() => { 
							
							switch (this.state.bt) {
							  case "5":  return (<form action=""> 
														
														<a className="waves-effect waves-teal btn" id="4" onClick={clickSt.bind(this)}>4({this.props.board.AvailableShips[3].toString()})</a> 
														<a className="waves-effect waves-teal btn" id="3" onClick={clickSt.bind(this)}>3({this.props.board.AvailableShips[2].toString()})</a> 
														<a className="waves-effect waves-teal btn" id="2" onClick={clickSt.bind(this)}>2({this.props.board.AvailableShips[1].toString()})</a> 
														<a className="waves-effect waves-teal btn" id="1" onClick={clickSt.bind(this)}>1({this.props.board.AvailableShips[0].toString()})</a>
														<p><a className="waves-effect waves-teal btn" id="0"onClick={clickSt.bind(this)}>Delete</a>
														<a className="waves-effect red lighten-2 btn" id="5" onClick={clickSt.bind(this)}>Rotate</a></p> 
													</form>);
							  case "4":	return (
													<form action=""> 
														<a className="waves-effect red lighten-2 btn" id="4" onClick={clickSt.bind(this)}>4({this.props.board.AvailableShips[3].toString()})</a> 
														<a className="waves-effect waves-teal btn" id="3" onClick={clickSt.bind(this)}>3({this.props.board.AvailableShips[2].toString()})</a> 
														<a className="waves-effect waves-teal btn" id="2" onClick={clickSt.bind(this)}>2({this.props.board.AvailableShips[1].toString()})</a> 
														<a className="waves-effect waves-teal btn" id="1" onClick={clickSt.bind(this)}>1({this.props.board.AvailableShips[0].toString()})</a> 
														<p><a className="waves-effect waves-teal btn" id="0"onClick={clickSt.bind(this)}>Delete</a>
														<a className="waves-effect waves-teal btn" id="5" onClick={clickSt.bind(this)}>Rotate</a></p>
													</form>
												);
							  case "3":  return (
													<form action=""> 
														
														<a className="waves-effect waves-teal btn" id="4" onClick={clickSt.bind(this)}>4({this.props.board.AvailableShips[3].toString()})</a> 
														<a className="waves-effect red lighten-2 btn" id="3" onClick={clickSt.bind(this)}>3({this.props.board.AvailableShips[2].toString()})</a> 
														<a className="waves-effect waves-teal btn" id="2" onClick={clickSt.bind(this)}>2({this.props.board.AvailableShips[1].toString()})</a> 
														<a className="waves-effect waves-teal btn" id="1" onClick={clickSt.bind(this)}>1({this.props.board.AvailableShips[0].toString()})</a> 
														<p><a className="waves-effect waves-teal btn" id="0"onClick={clickSt.bind(this)}>Delete</a>
														<a className="waves-effect waves-teal btn" id="5" onClick={clickSt.bind(this)}>Rotate</a></p> 
													</form>
													);
							  case "2":  return (
													<form action=""> 
														
														<a className="waves-effect waves-teal btn" id="4" onClick={clickSt.bind(this)}>4({this.props.board.AvailableShips[3].toString()})</a> 
														<a className="waves-effect waves-teal btn" id="3" onClick={clickSt.bind(this)}>3({this.props.board.AvailableShips[2].toString()})</a> 
														<a className="waves-effect red lighten-2 btn" id="2" onClick={clickSt.bind(this)}>2({this.props.board.AvailableShips[1].toString()})</a> 
														<a className="waves-effect waves-teal btn" id="1" onClick={clickSt.bind(this)}>1({this.props.board.AvailableShips[0].toString()})</a> 
														<p><a className="waves-effect waves-teal btn" id="0"onClick={clickSt.bind(this)}>Delete</a>
														<a className="waves-effect waves-teal btn" id="5" onClick={clickSt.bind(this)}>Rotate</a></p>  
													</form>
													);
							  case "1":  return (
													<form action=""> 
														
														<a className="waves-effect waves-teal btn" id="4" onClick={clickSt.bind(this)}>4({this.props.board.AvailableShips[3].toString()})</a> 
														<a className="waves-effect waves-teal btn" id="3" onClick={clickSt.bind(this)}>3({this.props.board.AvailableShips[2].toString()})</a> 
														<a className="waves-effect waves-teal btn" id="2" onClick={clickSt.bind(this)}>2({this.props.board.AvailableShips[1].toString()})</a> 
														<a className="waves-effect red lighten-2 btn" id="1" onClick={clickSt.bind(this)}>1({this.props.board.AvailableShips[0].toString()})</a> 
														<p><a className="waves-effect waves-teal btn" id="0"onClick={clickSt.bind(this)}>Delete</a> 
														<a className="waves-effect waves-teal btn" id="5" onClick={clickSt.bind(this)}>Rotate</a></p>
													</form>
													);
							  default:   return (
													<form action=""> 
														 
														<a className="waves-effect waves-teal btn" id="4" onClick={clickSt.bind(this)}>4({this.props.board.AvailableShips[3].toString()})</a> 
														<a className="waves-effect waves-teal btn" id="3" onClick={clickSt.bind(this)}>3({this.props.board.AvailableShips[2].toString()})</a> 
														<a className="waves-effect waves-teal btn" id="2" onClick={clickSt.bind(this)}>2({this.props.board.AvailableShips[1].toString()})</a> 
														<a className="waves-effect waves-teal btn" id="1" onClick={clickSt.bind(this)}>1({this.props.board.AvailableShips[0].toString()})</a> 
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