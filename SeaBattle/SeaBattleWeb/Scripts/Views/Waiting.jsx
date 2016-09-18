var Waiting = React.createClass({
	 getInitialState: function() {
		
        return {waitCtrl: new WaitingStateCtrl()};
    },
	checkGame: function()
	{	if(!this.state.waitCtrl.isFounded){
			this.state.waitCtrl.waiting();
		}
		else
		{
			this.props.stateCtrl.gamePreparing(this.state.waitCtrl.enemyName);
			this.props.onUpdate();
		}

	},
	componentDidMount: function() {
		window.setInterval(this.checkGame, 2000);
	},
    render: function(){
            return(
					<div>
						<br/>
						<div className="row center"><h5>Waiting for the second player</h5></div>
						<div className="progress">
							  <div className="indeterminate"></div>
						 </div>
						 <div className="row center">
						<a className="waves-effect waves-light btn" onClick={this.goBackClick}>Go Back</a> 
						</div> 
					 </div>
                   )
            },
			goBackClick: function(e)
			{	
				this.props.stateCtrl.waitCtrl.exit();
				this.props.stateCtrl.openMenu();
				this.props.onUpdate();
			} 
});