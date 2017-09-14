var CompanyGridRow = React.createClass({
    render: function() {
        return (
            <tr>
                <td>{this.props.item.name}</td>
                <td>{this.props.item.code}</td>
            </tr>
        );
    }
});

var CompanyGridTable = React.createClass({

    getInitialState: function() {
        return{
            items: []
        }
    },

    loadCompaniesFromServer: function() {
        $.get(this.props.dataUrl,
            function(data) {
                if (this.isMounted()) {
                    this.setState({
                        items: data
                    });
                }
            }.bind(this));
    },

    componentDidMount: function() {
        this.loadCompaniesFromServer();
    },

    render: function () {
        var rows = [];
        this.state.items.forEach(function (item) {
            rows.push(<CompanyGridRow key={item.id} item={item}/>);
        });
        return (<div className="table">
            <table >
                <thead>
                <tr>
                    <th>Company</th>
                    <th>Value</th>
                    <th>Actions</th>
                </tr>
                </thead>
                <tbody>
                    {rows}
                </tbody>
            </table>
                </div>
        );
    }
});

ReactDOM.render(
    <CompanyGridTable dataUrl="/companies/get"/>,
    document.getElementById('content')
);
