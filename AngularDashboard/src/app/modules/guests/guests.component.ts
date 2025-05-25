import { Component, ViewEncapsulation } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
    selector     : 'guests',
    standalone   : true,
    templateUrl  : './guests.component.html',
    imports      : [RouterModule],
    encapsulation: ViewEncapsulation.None,
})
export class GuestsComponent
{
    /**
     * Constructor
     */
    constructor()
    {
    }
}
