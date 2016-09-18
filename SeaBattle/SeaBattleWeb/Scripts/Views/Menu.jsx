﻿var Menu = React.createClass({
	render: function(){
		return(
                <div>
					<br/>
					<br/>
					<div className="row right">
						<a className="waves-effect waves-teal btn-flat" onClick={this.logOutClick}>Log out</a>        
					</div> 
					<div className="row center">
						<a className="waves-effect waves-teal btn-flat" onClick={this.createGameClick}>Create a game</a>        
					</div> 
					<div className="row center">
						<a className="waves-effect waves-teal btn-flat" onClick={this.joinGameClick}>Join the game</a> 
					</div> 
					<div className="row center">
						<a className="waves-effect waves-teal btn-flat" onClick={this.satatisClick}>Statistics</a> 
					</div>   
					<div className ="row center">
						<img className="logo" src = 'http://3.bp.blogspot.com/-7BVuNuOwZNM/VUae7-kKSWI/AAAAAAAABvk/DA7XsKrNFdY/s1600/anchor-clipart-anchor_silhouette.png'/>
					</div>					
                </div>
        )
    }, 
	 logOutClick: function(e)
    { 
		e.preventDefault();
		
    },   
    createGameClick: function(e)
    { 
		e.preventDefault();
		this.props.stateCtrl.createGame();
		this.props.onUpdate();
    },
	
	joinGameClick: function(e)
	{
		e.preventDefault();
		this.props.stateCtrl.joinGame();
		this.props.onUpdate();
	},
	
	statisticsClick: function(e)
	{
	}

});