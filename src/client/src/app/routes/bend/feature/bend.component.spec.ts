import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BendComponent } from './bend.component';

describe('BendComponent', () => {
  let component: BendComponent;
  let fixture: ComponentFixture<BendComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BendComponent]
    });
    fixture = TestBed.createComponent(BendComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
