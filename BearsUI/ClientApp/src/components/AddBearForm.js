import React, {  Component } from 'react'
import axios from 'axios';

class AddBearForm extends Component {

    state =  { id: null, name: '', typeName: '' }
    
    AddBear(bear,action) {
        axios.post('/bears', {
            name: bear.name,
            typeName: bear.typeName
        })
            .then(function (response) {
              
                action();
            })
            .catch(function (error) {
                console.log(error);
            });
       
    }
   
    render() {
        
       
       const handleInputChange = event => {
           const { name, value } = event.target
        
           this.setState( { [name]: value } )
        }
        

        return (
            <form
                onSubmit={event => {
                    event.preventDefault()
                    if (!this.state.name || !this.state.typeName) return

                    this.AddBear(this.state,this.props.action)
                   
                    var bear = { id: null, name: '', typeName: '' }
                    this.setState( bear )
                }}>
            
                <label>Name</label>
                <input type="text" name="name" value={this.state.name} onChange={handleInputChange} />
                <label>Type</label>
                <input type="text" name="typeName" value={this.state.typeName} onChange={handleInputChange} />
                <button>Add new bear</button>
            </form>
        )
    }
}

export default AddBearForm