var CompanyGridRow = React.createClass({
    render: function() {
        return (
            <tr>
                <td>{this.props.item.name}</td>
                <td>{this.props.item.price}</td>
                <td><button type="button" className="btn btn-secondary">Buy</button></td>
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

    componentDidMount: function () {
        var self = this;
        this.loadCompaniesFromServer();
        setInterval(() => self.loadCompaniesFromServer(), 10000);
    },

    render: function () {
        var rows = [];
        this.state.items.forEach(function (item) {
            rows.push(<CompanyGridRow key={item.id} item={item}/>);
        });
        return (<div>
            <table className="table table-responsive w-100 d-block d-md-table">
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
