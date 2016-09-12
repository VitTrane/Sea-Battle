
var LogIn = React.createClass({

    handleLoginChange: function(e) {
    this.setState({login: e.target.value});
    },
    handlePasswordChange: function(e) {
    this.setState({password: e.target.value});
    },

	render: function(){
		return(
                <div id="signin">   
				<h2>Welcome Back!</h2>   
				<form action="/" method="post" onSubmit={this.handleLogin}>
					<div className="input-field col s6">
						<input id="login1" type="text"  pattern=".{1,16}" title="1-16 characters" class="validate" required autoComplete ="off" onChange={this.handleLoginChange}>
						</input>
						<label for="login1">Login</label>
				
					</div>
					<div className="input-field col s12">
						<input id="password1" type="password" pattern=".{6,16}" title="6-16 characters" class="validate"  required autoComplete="off" onChange={this.handlePasswordChange}>
						</input>
						<label for="password1">Password</label>
						
				    </div> 
					<button className="btn waves-effect waves-light" type="submit" name="action">Log In
						<i className="material-icons right">send</i>
					</button>
				</form>
			</div>
)},

	goToMenu: function(data)
	{
		if(data.status)
		{
			React.render(<Menu/>, document.getElementById('content'));
		}
		else
		{
			alert(data.message);
		}   
	},

    handleLogin: function(e) {
    e.preventDefault()
      var d = {
      login    : this.state.login,
      password : this.state.password,
      email : ""
    }
    $.ajax({
                type:"POST",
                url : "/home/UserLogin",
                data : d,
                success: function(data){
				    this.setState({
                        login : '',
                        password  : ''
                    });
					this.goToMenu(data);					
                }.bind(this),                
                error : function(e){
                    console.log(e);
                    alert('Error! Please try again');
                }
            });
  }

});  
