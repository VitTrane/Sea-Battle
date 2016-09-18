var Games = React.createClass({
  render: function() {
		var createItem = function(item) {
		  return (
					<tr>
						<th data-field="id" value = {item.Creator}>item.Сreator.toString()</th>
						<th data-field="date" value = {item.CreationDate}>item.CreationDate.toString</th>
				    </tr>
				);
		};
		return this.props.games.map(createItem.bind(this));		

	}
});

var JoinGame = React.createClass({
  getInitialState: function() {
     return {joinCtrl: this.props.stateCtrl.joinCtrl};
    },
	getGames: function() {

			this.state.joinCtrl.getGames();
			this.onUpdate();
	},
	onUpdate: function(){
		 this.setState({joinCtrl: this.props.stateCtrl.joinCtrl});
	},
	componentDidMount: function() {
    
		window.setInterval(this.getGames, 3000);
	},
	joinGameClick: function(e)
	{	e.preventDefault();
		this.state.JoinCtrl.enemyInfo = e.currentTarget.value;
		this.state.JoinCtrl.joinGame();
		this.state.JoinCtrl.gamePreparing(e.currentTarget.value.Creator);
		this.props.onUpdate();
	},
    render: function(){
	var t = this.state.joinCtrl.games;
	getRows = function(item,i)
	{
		return(
			<div className="row center">
				<a className="waves-effect waves-teal btn-flat" onClick={this.joinGameClick} value ={item}>Join to {item.Creator} ({item.CreationDate})</a>
			</div>
		);	
	};
    return(
		<div>
				<h5 className="center-align">Available games</h5>
				{
					t.map(getRows.bind(this))
				}
				
		</div>);
}
});
