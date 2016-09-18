
var TopPlayers = React.createClass({
    render: function(){
	var getRows = function(item,i)
	{
		return(
			<tr>
				<td>{item.UserName}</td>
				<td>{item.GameCount}</td>
				<td>{item.WinRate}</td>
				<td>{item.RegistrationDate}</td>
			</tr>
		);	
	};
	var t = this.props.players;
    return(
		<div>
				
			<table>
				<thead>
				  <tr>
					  <th data-field="user">User</th>
					  <th data-field="games">Game Count</th>
					  <th data-field="win">Win Rate</th>
					  <th data-field="reg">Registration Date</th>
				  </tr>
				</thead>
				<tbody>
				{
					t.map(getRows.bind(this))
				}
				</tbody>
			</table>
		</div>
		);
}
});
