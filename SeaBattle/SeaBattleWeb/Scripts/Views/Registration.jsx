var Registration = React.createClass({
    render: function(){
        return(
			
		<div>
			<div className="col s4 m4 l4 right">
			<ul className="tabs" id ="tabLogOrReg">
				  <li className="tab col"><a className="black-text" href="#signin">Log in</a></li>
				  <li className="tab col"><a className="black-text" href="#signup">Sign Up</a></li>
					<div id ="tabUnderline" className="indicator teal lighten-4"></div>
			 </ul>
			 </div>
			<div id= "signin">
				<LogIn/>
			</div>

			<div id="signup">   
				<SignUp/>
			</div>

		</div>
		)
	}
		

});