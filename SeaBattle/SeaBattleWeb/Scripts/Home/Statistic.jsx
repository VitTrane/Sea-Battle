var Registration = React.createClass({
    render: function(){
        return(
			
		<div>
			<div className="col s4 m4 l4 right">
			<ul className="tabs" id ="tabLogOrReg">
				  <li className="tab col"><a className="black-text" href="#lastGames">Last 100 games</a></li>
				  <li className="tab col"><a className="black-text" href="#topPlayers">Top players</a></li>
					<div id ="tabUnderline" className="indicator teal lighten-4"></div>
			 </ul>
			 </div>
			<div id= "lastGames">
				<LastGames/>
			</div>

			<div id="topPlayers">   
				<TopPlayers/>
			</div>

		</div>
		)
	}
		

});