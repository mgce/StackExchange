var LoginForm = React.createClass({
    getInitialState: function () {
        return {
            Username: "",
            Password: "",
            Fields: []
        }
    },
    handleSubmit: function (e) {
        e.preventDefault();
        var validForm = true;
        this.state.Fields.forEach(function (field) {
            if (typeof field.isValid === 'function') {
                var validField = field.isValid(field.refs[field.props.name]);
                validForm = validForm && validField;
            }
        });

        if (validForm) {
            var loginData = {
                Username: this.state.Username,
                Password: this.state.Password
            }


            $.ajax({
                type: "POST",
                url: this.props.dataUrl,
                data: JSON.stringify(loginData),
                dataType: "json",
                Accept: "application/json",
                contentType: "application/json",
                success: function (data) {
                    this.setState(
                        this.getInitialState()
                    );
                    document.location.href = '/home'
                }.bind(this),
                error: function (response) {
                    console.log(response);
                    alert('Error! Please try again');
                }
            });
        }
    },
    onChangeUsername: function (value) {
        this.setState({
            Username: value
        });
    },
    onChangePassword: function (value) {
        this.setState({
            Password: value
        })
    },
    login: function (field) {
        var s = this.state.Fields;
        s.push(field);
        this.setState({
            Fields: s
        });
    },
    render: function () {
        return (
            <form name="loginForm" id="login-form" onSubmit={this.handleSubmit}>
                <InputField type={"text"} value={this.state.Username} label={"Username"} name={"Username"} htmlfor={"Username"} isRequired={true}
                    onChange={this.onChangeUsername} onComponentMounted={this.login} messageRequired={'Username required'} />
                <InputField type={"password"} value={this.state.Password} label={"Password"} name={"Password"} htmlfor={"Password"} isRequired={true}
                    onChange={this.onChangePassword} onComponentMounted={this.login} messageRequired={'Password required'} />
                <div className="form-group">
                    <div className="row  h-100 justify-content-center align-items-center">    
                        <div className="col-lg-6 col-md-6">
                            <button type="submit" value="post" id="login-submit" className="form-control btn btn-login" >
                                    Login
                            </button>
                         </div>
                    </div>
                </div>
            </form>
        );
    }
});

ReactDOM.render(
    <LoginForm dataUrl="/Account/SignIn" />,
    document.getElementById('loginForm')
)