var SignUp = React.createClass({
	handleLoginChange: function(e) {
    this.setState({login: e.target.value});
    },
	handleEmailChange: function(e) {
    this.setState({email: e.target.value});
    },
    handlePasswordChange: function(e) {
    this.setState({password: e.target.value});
    },

	render: function(){
		return(
                <div>   
				<h2>Sign Up</h2>   
				<form action="/" method="post" onSubmit={this.handleSignup}>
					<div className="input-field col s6">
						<input id="login" type="text"  pattern=".{1,16}" title="1-16 characters" class="validate" required autoComplete ="off" onChange={this.handleLoginChange}>
						</input>
						<label for="login">Login <span class="req">*</span></label>
						
					</div>
				    <div className="input-field col s12">
						<input id="email" type="email" pattern ="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$" title="example@mail.com" 
								class="validate" required="true" autoComplete ="off" onChange={this.handleEmailChange}>
						</input>
						<label for="email">Email Address <span class="req">*</span></label>
						
				    </div> 
					<div className="input-field col s12">
						<input id="password" type="password" pattern=".{6,16}" title="6-16 characters" class="validate"  required="true" autoComplete="off" onChange={this.handlePasswordChange}>
						</input>
						<label for="password"> Set a password<span class="req">*</span></label>
						
				    </div> 
					<button className="btn waves-effect waves-light" type="submit" name="action">Get Started
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

 handleSignup: function(e) {
    e.preventDefault()
      var d = {
      login    : this.state.login,
      password : this.state.password,
      email : this.state.email
    }
    $.ajax({
                type:"POST",
                url : "/home/UserRegister",
                data : d,
                success: function(data){
				    this.setState({
                        login : '',
                        password  : '',
						email  : ''
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
