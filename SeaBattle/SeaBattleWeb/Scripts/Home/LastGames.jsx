var LastGames = React.createClass({
getInitialState: function() {
         $.ajax({
                type:"GET",
                url : "/home/UserRegister",
                data : d,
                success: function(data){
				    this.setState({
                       if(data.status)
                       {
                       }
                       else
                       {
                       }

                    });
					this.goToMenu(data);					
                }.bind(this),                
                error : function(e){
                    console.log(e);
                    alert('Error! Please try again');
                }
            });     
        return {lastGames: data;};
    },
    render: function(){
        return(
            <table>
        <thead>
          <tr>
              <th data-field="id">Name</th>
              <th data-field="name">Item Name</th>
              <th data-field="price">Item Price</th>
          </tr>
        </thead>

        <tbody>
          <tr>
            <td>Alvin</td>
            <td>Eclair</td>
            <td>$0.87</td>
          </tr>
          <tr>
            <td>Alan</td>
            <td>Jellybean</td>
            <td>$3.76</td>
          </tr>
          <tr>
            <td>Jonathan</td>
            <td>Lollipop</td>
            <td>$7.00</td>
          </tr>
        </tbody>
      </table>
);
}
});