var Messages = React.createClass({
  render: function() {
		var createItem = function(item) {
		  return <p key={item.id}>{item.text}</p>;
		};
		return <div id="chatTextArea">{this.props.items.map(createItem)}</div>;		

	}
});

var Game = React.createClass({
    
    getInitialState: function() {
         var arr = [];
            for(i=0; i< 10; i++)
            {
	            arr[i]=[];
	            for(j=0; j< 10; j++)
	            {
		            arr[i][j] = {status: 'clean', x: j, y: i};
	            }
            }
        return {itemsMessages: [], text: '', playerCells:arr, enemyCells:arr, isReady: false};
    },

    render: function(){
            return(
                   <div>
                      <div className="row">
					        <div className="col left" id = "Player">
                                 <PlayField cells={this.state.playerCells} isPlayer={true}/>
					         </div>
                            <div className="col right"  id = "Enemy">
                                <PlayField cells={this.state.enemyCells} isPlayer={false}/>
					        </div>
                       </div> 
                       <div className="row">      
                            <div id = "Chat">
                                <h5>Chat:</h5>
								<div id="chatContent">
									<Messages items={this.state.itemsMessages}></Messages> 
								</div>
                                <form action="/" method="post" onSubmit={this.handleSend}>
									<div className="input-field col s12">
										<input id="message" type="text" pattern=".{1,1024}" title="от 1 до 1024 символов" class="validate"  required="true" autoComplete="off" 
											value={this.state.text} onChange={this.handleMessageChange}>
										</input>						
									</div> 
                                    <div className="row right">
									    <button className="btn waves-effect waves-light" type="submit" name="action">Send</button>
                                        <a className="waves-effect waves-light btn" onClick={this.goBackClick}>Exit</a> 
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
			 e.preventDefault()
             var d ={text: this.state.text}
                $.ajax({
                            type:"POST",
                            url : "/home/SendMessage",
                            data : d,
                            success: function(data){
                              this.addMessage(data);  
					            					
                            }.bind(this),                
                            error : function(e){
                                console.log(e);
                                alert('Error! Please try again');
                            }
                        });
              },
             addMessage: function(data)
             {
                if(data.status)
				{
					var mes = "You: " + this.state.text;
					var nextItems = this.state.itemsMessages.concat([{text:mes, id: Date.now()}]);
                    var nextText = '';
                    this.setState({itemsMessages: nextItems, text: nextText});
				}
				else
				{
					alert(data.message);
				}   
             },
			goBackClick: function(e)
			{	
				React.render(<Menu/>, document.getElementById('content'));
			} 
});