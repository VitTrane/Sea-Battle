var Statistics = React.createClass({
	getInitialState: function() {
    return {stCtrl: this.props.stateCtrl.statisticCtrl, isGame:true, fisInterested:true};   
    },
	componentDidMount: function() {
		
	},
	getLastData: function(){
		this.state.stCtrl.getGames();
		this.state.stCtrl.getPlayers();
		this.onUpdate();
	}, 
	componentDidMount: function() {
		this.getLastData();
	},
	onUpdate: function(){
		 this.setState({stCtrl: this.props.stateCtrl.statisticCtrl});
	},
    render: function(){
	var exitClick = function(e)
	{	
		e.preventDefault();
		this.props.stateCtrl.openMenu();
		this.props.onUpdate();
	};
	var btClick = function(e)
	{
		if(e.currentTarget.id == 1)
		{
			this.getLastData();
			this.state.isGame = true;
			this.onUpdate();
		}
		else
		{	this.getLastData();
			this.state.isGame = false;
			this.onUpdate();
		}
	};
		if(this.state.isGame)
		{
			return(	
				<div>
					<div className="row">
						<a className="waves-effect waves-light btn" id = "1"onClick={btClick.bind(this)}>Last 100 games</a> 
						 <a className="waves-effect waves-light btn"id="2" onClick={btClick.bind(this)}>Top Players</a> 
						<a className="waves-effect waves-light btn" onClick={exitClick.bind(this)}>Go Back</a>        
					 </div>
					 <LastGames games={this.state.stCtrl.games}/>
				</div>
			);
		}
		else
		 {
			return(	
				<div>
					<div className="row">
						<a className="waves-effect waves-light btn" id = "1"onClick={btClick.bind(this)}>Last 100 games</a> 
						 <a className="waves-effect waves-light btn"id="2" onClick={btClick.bind(this)}>Top Players</a> 
						<a className="waves-effect waves-light btn" onClick={exitClick.bind(this)}>Go Back</a>        
					 </div>
					 <TopPlayers players={this.state.stCtrl.players}/>
				</div>
			);
		 }

	}
		

});