var Messages = React.createClass({
  render: function() {
		var createItem = function(item) {
		  return <p key={item.id}>{item.text}</p>;
		};
		return <div id="chatTextArea">{this.props.items.map(createItem.bind(this))}</div>;		

	}
});

var Game = React.createClass({
    
    getInitialState: function() {
		
        return {text: '', gameCtrl: this.props.stateCtrl.gameCtrl};
    },
	
	getMessagesAndShots: function() {
		if(this.state.gameCtrl.isStarted)
		{
			this.state.gameCtrl.ListenToMessage();
			this.state.gameCtrl.takeShot();
			this.onUpdate();
		}
	},
	onUpdate: function(){
		 this.setState({gameCtrl: this.props.stateCtrl.gameCtrl});
	},
	componentDidMount: function() {
    
		window.setInterval(this.getMessagesAndShots, 3000);
	},
	getPlayField: function()
			{
				var startGame = function()
					{
						if(this.state.gameCtrl.playerBoard.isReadyForGame()){
							this.state.gameCtrl.start();
							this.onUpdate();
								}
						else{
							alert('Not all ships are placed');
						}
					};
					
				if(this.state.gameCtrl.isStarted)
				{
					return(
						<PlayerField board={this.state.gameCtrl.playerBoard} onUpdate = {this.onUpdate}/>
					);
				}
				else
				{
					return(<div>
							<PlayPreparingField board={this.state.gameCtrl.playerBoard} onUpdate = {this.onUpdate}/>
								<a className="waves-effect waves-teal btn" onClick={startGame.bind(this)}>Start</a>	
							</div>

					);
				}
			},
    render: function(){
			var goBackClick = function(e)
					{	
						this.props.stateCtrl.gameCtrl.exit();
						this.props.stateCtrl.openMenu();
						this.props.onUpdate();
					};
			
            return(
                <div className="row">
					<div className="col left" id = "Player">
									<h5>You</h5>
									{
										this.getPlayField()
									}
									</div>
								<div className="col center"  id = "Enemy">
									<h5>{this.state.gameCtrl.enemyName}</h5>
									<EnemyField  board={this.state.gameCtrl.enemyBoard} onUpdate = {this.onUpdate} onShot = {this.state.gameCtrl.shot}/>
								</div>
					<div className="col right">      
						<div id = "Chat">
							<h5>Chat:</h5>
							<div id="chatContent">
								<Messages items={this.state.gameCtrl.messages}></Messages> 
							</div>
							<form action="/" method="post" onSubmit={this.handleSend}>
								<div className="input-field col s12">
									<input id="message" type="text" pattern=".{1,1024}" title="от 1 до 1024 символов" class="validate"  required="true" autoComplete="off" 
										value={this.state.text} onChange={this.handleMessageChange}>
									</input>						
									</div> 
									<div className="row right">
										<button className="btn waves-effect waves-light" type="submit" name="action">Send</button>
										<a className="waves-effect waves-light btn" onClick={goBackClick.bind(this)}>Exit</a> 
									</div>
							</form>
						</div>
					</div>
                </div>                  
                   )
            },
			handleMessageChange: function(e) {
				this.setState({text: e.target.value});
			},
			handleSend: function(e)
			{ 
			 e.preventDefault();
			 this.props.stateCtrl.gameCtrl.sendMessage(this.state.text);
			 this.onUpdate();
			 this.setState({text:''}); 
            }

			
});