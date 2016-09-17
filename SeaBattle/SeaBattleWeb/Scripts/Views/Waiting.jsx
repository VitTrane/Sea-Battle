var Waiting = React.createClass({
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
				React.render(<Menu/>, document.getElementById('content'));
			} 
});