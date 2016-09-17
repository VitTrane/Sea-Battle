var App = React.createClass({
  getInitialState: function() {
        return {stateCtrl:this.props.stateCtrl};
  },
  onStUpdate: function() {
       this.setState({stateCtrl: this.props.stateCtrl});
    },
	render: function(){
	  switch(this.state.stateCtrl.state){
	  case 0:
	    return <Registration stateCtrl = {this.state.stateCtrl} onUpdate = {this.onStUpdate}/>;
	   case 1:
	     return <Menu stateCtrl = {this.state.stateCtrl} onUpdate = {this.onStUpdate}/>;
	   case 2:
	      return <Waiting stateCtrl = {this.state.stateCtrl} onUpdate = {this.onStUpdate}/>;
	   case 3:
	     return <JoinGame stateCtrl = {this.state.stateCtrl} onUpdate = {this.onStUpdate}/>;
	   case 4:
         /*this.state.stateCtrl.InitGame();*/
	     return <Game stateCtrl = {this.state.stateCtrl} onUpdate = {this.onStUpdate}/>;
	   case 5:
	     return <Statistics stateCtrl = {this.state.stateCtrl} onUpdate = {this.onStUpdate}/>;
	  }
	}
});