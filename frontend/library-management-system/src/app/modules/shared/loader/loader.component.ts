import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoaderService } from '../../../shared/services/loader.service';


@Component({
  selector: 'app-loader',
  standalone: true,
  imports: [
    CommonModule
],
  template: `
    @if (loaderService.isLoading | async) {
      <div class="loader-backdrop">
        <div class="spinner"></div>
      </div>
    }
    `,
  styleUrls: ['./loader.component.scss']
})
export class AppLoaderComponent {
  constructor(public loaderService: LoaderService) {}
}