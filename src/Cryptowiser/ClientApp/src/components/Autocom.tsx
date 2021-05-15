import React, { ChangeEvent, Component } from 'react';  
import TextField from '@material-ui/core/TextField';  
import Autocomplete from '@material-ui/lab/Autocomplete';  
import '../../src/custom.css';  
import axios from 'axios';  
import { authHeader } from '../_helpers/auth-header';

interface IProps {
}

interface ISymbolRate{
  name: string;
  priceDetail: IPriceDetail ;
}
interface IPriceDetail{
  price: number;
  lastUpdated: Date ;
}
interface IAutocomState {
    Symbols: string[];
    Symbol: string;
    rates: ISymbolRate[];
    errorMessage: string;
}

export class Autocom extends Component<IProps, IAutocomState> { 
        constructor(props: IProps) {  
          super(props)  
          this.state = {  
                  Symbols: [],
                  Symbol: "",
                  rates: [],
                  errorMessage: ""
          }
          
          this.onCurrencyChange = this.onCurrencyChange.bind(this);
        }  

        componentDidMount() {  
            const headers = authHeader();
              axios.get('api/crypto/symbols/market_cap', { headers })
              .then(response => {
                this.setState({  
                    Symbols: response.data  
                });  
              })
              .catch(error => {
                  console.error('There was an error!', error);
              });
        }  

        onCurrencyChange = (event: ChangeEvent<{}>, values: string | null) => {
            this.setState({
                Symbol: values ?? ""
              }, () => {
                console.log(this.state.Symbol);
                this.populateCryptoRates(this.state.Symbol);
            });
          }
          

        static renderCryptoRatesTable(symbol: string, symbolRates: ISymbolRate[]) {
          return (
            <div>
              <br/>
              <p><em>Showing Rates for {symbol}</em></p> 
              <br/>
              <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                  <tr>
                    <th>Name</th>
                    <th>Price</th>
                    <th>Last Updated</th>
                  </tr>
                </thead>
                <tbody>
                  {symbolRates.map(symbolRate =>
                    <tr key={symbolRate.name}>
                      <td>{symbolRate.name}</td>
                      <td>{symbolRate.priceDetail.price}</td>
                      <td>{symbolRate.priceDetail.lastUpdated}</td>
                    </tr>
                  )}
                </tbody>
              </table>
            </div>
          );
        }

        render() {  
                
          let contents = this.state.Symbol
          ? Autocom.renderCryptoRatesTable(this.state.Symbol , this.state.rates)
          : <p><em>Please Search and Select Crypto Currency Above!</em></p>;
          
          let refreshLink = this.state.Symbol ?
          <a href="#" onClick={e=> this.populateCryptoRates(this.state.Symbol)}>Refresh</a>
          : null;

          return (  
            <div>  
              <table>
                <tr>
                  <td>
                    <Autocomplete className="pding"  
                      id="combo-box-demo"  
                      onChange={this.onCurrencyChange}
                      options={this.state.Symbols}  
                      getOptionLabel={option => option}  
                      style={{ width: 300 }}  
                      renderInput={params => (  
                        <TextField {...params} label="Search Crypto Currency" variant="outlined" fullWidth /> 
                      )}  
                    />
                  </td>
                  <td> {refreshLink}</td>
                  </tr>
              </table>

              {contents}
            </div>  
            )  
        }
        
        async populateCryptoRates(symbol: string) {
            const headers = authHeader();
        
            axios.get('api/crypto/rates/' + symbol, { headers })
            .then(response => {
              this.setState({ rates: response.data});
            })
            .catch(error => {
                this.setState({ errorMessage: error.message });
                console.error('There was an error!', error);
            });
          }
}  
export default Autocom  



