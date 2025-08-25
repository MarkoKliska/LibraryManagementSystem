import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-view-toggle',
  imports: [
    CommonModule
  ],
  templateUrl: './view-toggle.component.html',
  styleUrl: './view-toggle.component.scss'
})
export class ViewToggleComponent {
  @Input() viewMode!: 'available' | 'rented';
  @Output() viewChange = new EventEmitter<'available' | 'rented'>();

  switchView(mode: 'available' | 'rented'): void {
    this.viewChange.emit(mode);
  }
}
