
var JoinGame = React.createClass({
  getInitialState: function() {
     return {joinCtrl: this.props.stateCtrl.joinCtrl, loocking: true};
    },
	getGames: function() {
	if(this.state.looking || this.state.looking === undefined){
			this.state.joinCtrl.getGames();
			this.onUpdate();
			}
	},
	onUpdate: function(){
		 this.setState({joinCtrl: this.props.stateCtrl.joinCtrl});
	},
	componentDidMount: function() {
		window.setInterval(this.getGames, 2000);
	},
    render: function(){
	var joinGameClick = function(e)
	{	this.state.looking =false;
		e.preventDefault();
		var i = e.currentTarget.id;
		this.state.joinCtrl.setEnemy(i);
		this.state.joinCtrl.joinGame();
		this.props.stateCtrl.gamePreparing(this.state.joinCtrl.enemyInfo.Creator);
		this.props.onUpdate();
	};
	var exitClick = function(e)
	{	this.state.looking =false;
		e.preventDefault();
		this.props.stateCtrl.openMenu();
		this.props.onUpdate();
	};
	var t = this.state.joinCtrl.games;
	var getRows = function(item,i)
	{
		return(
			<div className="row center">
				<a className="waves-effect waves-teal btn-flat" onClick={joinGameClick.bind(this)}  id={i} key = {i}>Join to {item.Creator} ({item.CreationDate})</a>
			</div>
		);	
	};
    return(
		<div>
				<h5 className="center-align">Available games</h5>
				{
					t.map(getRows.bind(this))
				}
				<div className="row center">
						<a className="waves-effect waves-light btn" onClick={exitClick.bind(this)}>Go Back</a>        
				</div>
		</div>);
}
});
