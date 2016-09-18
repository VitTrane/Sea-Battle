var LastGames = React.createClass({
render: function(){
	var getRows = function(item,i)
	{
		return(
			<tr>
				<td>{item.FirstPlayer}</td>
				<td>{item.SecondPlayer}</td>
				<td>{item.Winner}</td>
				<td>{item.DateStart}</td>
				<td>{item.Duration}</td>
			</tr>
		);	
	};
	var t = this.props.games;
    return(
		<div>
				
			<table>
				<thead>
				  <tr>
					  <th data-field="firstPlayer">First Player</th>
					  <th data-field="secondPlayer">Second Player</th>
					  <th data-field="winner">Winner</th>
					  <th data-field="dateStart">Start Date</th>
					  <th data-field="duration">Duration</th>
				  </tr>
				</thead>
				<tbody>
				{
					t.map(getRows.bind(this))
				}
				</tbody>
			</table>
				
		</div>);
}
});