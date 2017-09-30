var NavbarRightMenu = React.createClass({
    getInitialState: function () {
        return {
            Username: ""
        }
    },
    componentWillMount: function () {
        var data = this.secondGet();
    },
    getUsername: function () {
        fetch("/Account/GetUsername", { credential: 'test' }).then(response => {
            return response.json
        }).then(json => {
            this.setState({ Username: json });
         }).catch((error) => {
             console.log(error)
        })
    },
    secondGet: function () {
        $.get("/Account/GetUsername", { credential: 'test' }, function (response) {
            this.setState({ Username: response })
        }.bind(this))
    },
    render: function () {
        return (
            <div className="collapse navbar-collapse " id="navbarSupportedContent">
                <ul className="nav navbar-nav ml-auto">
                    <li className="nav-item">
                        <a className="nav-link">Logged as: <b>{this.state.Username}</b></a>
                    </li>
                    <li className="nav-item">
                        <a className="nav-link" href="#">
                            <span className="fa fa-cog" aria-hidden="true"></span>
                        </a>
                    </li>
                    <li className="nav-item">
                        <a className="nav-link" href="Account/logout">
                            <span className="fa fa-power-off"></span>
                        </a>
                    </li>
                </ul>
            </div>
        );
    }
});


ReactDOM.render(
    <NavbarRightMenu />,
    document.getElementById('navbarSupportedContent')
)