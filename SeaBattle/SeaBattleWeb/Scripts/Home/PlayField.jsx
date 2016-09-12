var Row = React.createClass({
	getInitialState: function() { 
        return {cells: []};
    },
	render: function(){
			return ( 
					<div className="FieldRow">
						 {this.props.cells.map(function(object, j) {
								switch(object.status)
								{
									case 'clean':
										return <div className="cell clean" status={object.status} y={object.y} x={object.x} onClick={this.cellClick} key={j}></div>;
									case 'miss':
										return <div className="cell miss" status={object.status} y={object.y} x={object.x} onClick={this.cellClick} key={j}></div>;
									case 'damaged':
										return <div className="cell damaged" status={object.status} y={object.y} x={object.x} onClick={this.cellClick} key={j}></div>;
									case 'killed':
										return <div className="cell killed" status={object.status} y={object.y} x={object.x} onClick={this.cellClick} key={j}></div>;
									default:
										return <div className="cell ship" status={object.status} y={object.y} x={object.x} onClick={this.cellClick} key={j}></div>;
								}
						})}
					</div>
					);		
	},
    cellClick: function(e)
    {
       this.props.cells[e.target.y][e.target.x].status = 'ship';
    }
});

var PlayField = React.createClass({	
	
	getInitialState: function() { 
        return {cells: this.props.cells, isReady: 'false', isPlayer: this.props.isPlayer};
    },	
	
  render: function() {
				var i = -1;
				return(
					 <div className="Field">
							{this.props.cells.map(function(item, i) {
							  i++;
							  return (
								<Row cells={item} key={i}></Row>
							  );
							})}
						</div>
					  )
          }

});