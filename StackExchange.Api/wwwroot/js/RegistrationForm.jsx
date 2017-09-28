var InputField = React.createClass({
    handleChange: function(e) {
        this.props.onChange(e.target.value);
        var isValid = this.isValid(e.target);
    },
    showError: function(input) {
        input.classList.add('error');
        //input.nextSibling.textContent = this.props.messageRequired;
    },
    hideError: function(input) {
        input.classList.remove('error');
        //input.nextSibling.textContent = ""; 
    },
    isValid: function(input) {
        if (input.getAttribute('required') != null && input.value === "") {
            this.showError(input);
            return false;
        } else {
            this.hideError(input);
        }
        if (input.getAttribute('type') != null && input.value === "") {
            this.showError(input);
            return false;
        } else {
            this.hideError(input);
        }
        return true;
    },
    validateEmail: function(input) {
        var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(input);
    },
    componentDidMount: function() {
        if (this.props.onComponentMounted) {
            this.props.onComponentMounted(this);
        }   
    },
    render: function() {        
        var inputField = <input type={this.props.type} value={this.props.value} ref={this.props.name} name={this.props.name} placeholder={this.props.name}
            className="form-control" required={this.props.isRequired} onChange={this.handleChange} />

        return (
            <div className="form-group">
                <label htmlFor={this.props.htmlFor}>{this.props.label}:</label>
                {inputField}
            </div>
        );
    }
});


var RegistrationForm = React.createClass({
    getInitialState : function() {
        return {
            FirstName : "",
            LastName : "",
            Username : "",
            Email : "",
            Password : "",
            ConfirmPassword: "",
            Fields : []
        }
    },
    handleSubmit: function(e) {
        e.preventDefault();

        var validForm = true;
        this.state.Fields.forEach(function(field) {
            if (typeof field.isValid === 'function') {
                var validField = field.isValid(field.refs[field.props.name]);
                validForm = validForm && validField;
            }
        });
        if (this.state.Password !== this.state.ConfirmPassword) {
            validForm = false;
        }
        if (validForm) {
            var registrationData = {
                FirstName: this.state.FirstName,
                LastName: this.state.LastName,
                Username: this.state.Username,
                Email: this.state.Email,
                Password: this.state.Password
            }


            $.ajax({
                type: "POST",
                url: this.props.dataUrl,
                data: JSON.stringify(registrationData),
                dataType: "json",
                Accept: "application/json",
                contentType: "application/json", 
                success: function(data) {
                    this.setState(
                        this.getInitialState()
                    );
                }.bind(this),
                error: function(response) {
                    console.log(response);
                    alert('Error! Please try again');
                }
            });
        }      
    },
    onChangeFirstName: function(value) {
        this.setState({
            FirstName: value
        });
    },
    onChangeLastName: function (value) {
        this.setState({
            LastName: value
        });
    },
    onChangeUsername: function (value) {
        this.setState({
            Username: value
        });
    },
    onChangeEmail: function (value) {
        this.setState({
            Email: value
        });
    },
    onChangePassword: function (value) {
        this.setState({
            Password: value
        });
    },
    onChangeConfirmPassword: function (value) {
        this.setState({
            ConfirmPassword: value
        });
    },
    register: function(field) {
        var s = this.state.Fields;
        s.push(field);
        this.setState({
            Fields: s
        });
    },
    render: function() {
        return (
            <form name="registrationForm" id="register-form" onSubmit={this.handleSubmit}>
                <p className="servermessage">{this.state.ServerMessage}</p>
                <InputField type={"text"} value={this.state.Username} label={"Username"} name={"User name"} htmlfor={"Username"} isRequired={true}
                    onChange={this.onChangeUsername} onComponentMounted={this.register} messageRequired={'Username required'} />

                <InputField type={"text"} value={this.state.Email} label={"Email"} name={"Email"} htmlfor={"Email"} isRequired={true}
                    onChange={this.onChangeEmail} onComponentMounted={this.register} messageRequired={'Email required'} />

                <InputField type={"text"} value={this.state.FirstName} label={"First Name"} name={"First Name"} htmlfor={"First Name"} isRequired={true}
                            onChange={this.onChangeFirstName} onComponentMounted={this.register} messageRequired={'First Name required'} />

                <InputField type={"text"} value={this.state.LastName} label={"Last Name"} name={"Last Name"} htmlfor={"Last Name"} isRequired={true}
                            onChange={this.onChangeLastName} onComponentMounted={this.register} messageRequired={'Last Name required'} />

                <InputField type={"password"} value={this.state.Password} label={"Password"} name={"Password"} htmlfor={"Password"} isRequired={true}
                    onChange={this.onChangePassword} onComponentMounted={this.register} messageRequired={'Password required'} />

                <InputField type={"password"} value={this.state.ConfirmPassword} label={"Confirm Password"} name={"Confirm Password"} htmlfor={"Confirm Password"} isRequired={true}
                    onChange={this.onChangeConfirmPassword} onComponentMounted={this.register} messageRequired={'Confirm Password required'} />

                <div className="form-group">
                    <div className="row">
                        <div className="col-lg-6 col-md-3">
                            <button type="submit" value="post" id="register-submit" className="form-control btn btn-register" >
                                Register Now
                            </button>
                        </div>
                    </div>
                </div>              
            </form>
        );
    }
});

ReactDOM.render(
    <RegistrationForm dataUrl="/Account/Register"/>,
    document.getElementById('registration')
)